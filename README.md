# Dog Breed Aggregator API

This project is a .NET Web API that aggregates and enriches dog breed information from multiple public APIs, providing a unified and filterable endpoint.

---

## Features

- Aggregates data from:
  - [dogapi.dog](https://dogapi.dog/)
  - [Dog CEO API](https://dog.ceo/dog-api/)
  - [Wikipedia](https://en.wikipedia.org/w/api.php)
  - [TheDogAPI](https://thedogapi.com/)
-  Pagination support
-  Filtering by name, breed group, temperament, hypoallergenic, etc.
-  Caching (in-memory) to reduce redundant external API calls
- Parallel API calls to improve performance
- Unit Tests for Controller

The `AggregationController` is covered by unit tests using [xUnit](https://xunit.net/) and [Moq](https://github.com/moq/moq) to ensure correct behavior of the endpoint logic.

### What We Test

- Successful response when data is returned from the aggregator service
- `BadRequest` is returned when model validation fails
- `500 Internal Server Error` is returned when an exception is thrown
- Filtering logic based on `DogBreedFilterDto` inputs

### Tools & Approach

- **Testing Framework**: xUnit
- **Mocking Library**: Moq
- **Assertions**: FluentAssertions
- **Test Scope**: Controller is tested in isolation by mocking all dependencies
  
- Swagger/OpenAPI documentation
