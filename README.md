# Traknova Telematics Ingestion Service

A small but production-shaped C#/.NET 8 service that ingests telematics device payloads, validates them, and persists them to SQL Server via Entity Framework Core.

---

## Architecture

The solution follows a clean layered architecture:

```
TraknovaTelematics/
├── src/
│   ├── TraknovaTelematics.Core/            # Domain: Entities, Interfaces, Services (no EF, no HTTP)
│   ├── TraknovaTelematics.Infrastructure/  # EF Core DbContext, Repositories, Migrations
│   └── TraknovaTelematics.API/             # ASP.NET Web API: Controllers, DTOs, DI wiring
└── tests/
    └── TraknovaTelematics.Tests/
        ├── Unit/         # Pure validation logic — no DB, instant feedback
        └── Integration/  # Full ingestion pipeline with EF InMemory provider
```

**Key design decisions:**

- **Core has zero infrastructure dependencies.** `TelematicsIngestionService` only knows about `ITelematicsRepository` — it can be tested without spinning up a database.
- **All payload fields are nullable.** Different device types report different signals. A record is valid so long as `VehicleId` and `TimeStamp` are present and sane.
- **Crash detection is an EF Owned Type**, stored flat in the same `TelematicsRecords` row rather than a separate table. It's always fetched together, never queried independently, so a join would be pure overhead.
- **Batch ingestion returns HTTP 207 Multi-Status** when some records pass and others fail, giving device clients precise per-record feedback.

---

## Database Schema

One primary table: `TelematicsRecords`

| Column Group | Notes |
|---|---|
| `Id` (bigint PK) | Auto-increment — safe for high-volume insert workloads |
| `VehicleId`, `TimeStamp`, `IngestedAt` | Core identity + audit columns |
| GPS columns | All nullable — devices without GPS lock report no values |
| CAN columns | CAN bus data — only present on supported vehicles |
| OBD columns | OBD-II data — alternative to CAN, never both on same record |
| Battery / Connectivity / Alerts | Nullable bool/int fields |
| `CrashImpactMagnitude`, `CrashAxis` | Flattened owned type columns |
| `TripId` (uniqueidentifier) | Nullable FK-candidate for a future `Trips` table |

**Indexes:**
- `IX_TelematicsRecords_VehicleId_TimeStamp` — primary query pattern (vehicle history)
- `IX_TelematicsRecords_TripId` — trip-based lookups

---

## Running Locally

### Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

### Option A — Docker Compose (API + DB together)

```bash
docker-compose up --build
```

API will be available at `http://localhost:5000`  
Swagger UI at `http://localhost:5000/swagger`

### Option B — DB in Docker, API locally

```bash
# 1. Start only the database
docker compose up db -d
# start the service itself
docker compose up
# 2. Run the API
cd src/TraknovaTelematics.API
dotnet run
```

The API auto-runs EF migrations on startup — no manual `dotnet ef` step needed.

---

## Running Tests

```bash
dotnet test
```

All tests use either pure in-memory logic or the EF InMemory provider — no Docker required to run the test suite.

---

## Ingesting the Sample Payload

```bash
curl -X POST http://localhost:5000/api/telematics/ingest \
  -H "Content-Type: application/json" \
  -d @payload.json
```

Expected response (all 5 records from the sample pass validation):

```json
{
  "accepted": 5,
  "rejected": 0,
  "errors": []
}
```

---

## If I Had More Time

**Production hardening I'd add:**

1. **Idempotency** — add a unique index on `(VehicleId, TimeStamp)` with `ON CONFLICT DO NOTHING` semantics (or SQL Server's `MERGE`), so replayed payloads don't create duplicates.

2. **Message queue ingestion** — in production, devices would publish to a queue (Azure Service Bus / Kafka). The ingestion service would be a background consumer, decoupling device uptime from API availability and enabling backpressure.

3. **Separate `Trips` table** — normalise `TripId` into its own table with start/end time, vehicle, and aggregate stats (distance, average EcoScore). The FK would enforce referential integrity.

4. **Alerts table** — rather than bool columns on every record, active alerts should be their own rows with `RaisedAt` / `ResolvedAt` timestamps to support alert history and duration queries.

5. **Time-series DB consideration** — at scale (millions of records/day) a time-series store (TimescaleDB, InfluxDB, or Azure Data Explorer) would outperform SQL Server for range queries over `TimeStamp`. The repository interface abstracts this — swapping the storage backend wouldn't touch the Core layer.

6. **Auth** — Bearer token / API key validation on the ingest endpoint. Devices would authenticate per-fleet.

7. **OpenTelemetry** — structured logging + metrics (ingestion throughput, rejection rate, latency p99) exported to a collector.

8. **Pagination** on `GET /telematics/{vehicleId}` — the current implementation loads all records into memory.
