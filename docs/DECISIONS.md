# Architecture Decisions

## ADR-001

### Title

Configuration Abstraction

### Decision

Configuration is accessed through an abstraction (`IConfigurationProvider`) instead of directly reading JSON.

### Reason

This allows future support for JSON, environment variables, cloud secrets, or other configuration providers without changing the framework consumers.

### Status

Accepted