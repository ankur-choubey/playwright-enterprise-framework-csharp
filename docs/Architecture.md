# Architecture

## Overview

The framework follows a layered architecture where each project has a single responsibility.

## Current Projects

- Framework.Core
- Framework.Common
- Framework.Tests

## Design Principles

- Separation of Concerns
- SOLID Principles
- Dependency Inversion
- Configuration Abstraction

## Future Projects

- Framework.UI
- Framework.API
- Framework.Reporting

## Browser Layer

The browser layer abstracts browser lifecycle management from the underlying automation library.

Current responsibilities:

- Browser startup
- Browser shutdown
- Session lifecycle

Future responsibilities:

- Browser context management
- Page lifecycle
- Parallel execution support