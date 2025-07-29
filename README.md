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
- Swagger/OpenAPI documentation
