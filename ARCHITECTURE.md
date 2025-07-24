# Introduction

This document contains all notes and information related to the architecture and design of this solution.

## Database Design

Below is an Entity Relationship Diagram for the database used in the solution.

![Database ER Diagram](/documentation/database-er.png)

The following assumptions were made for the database design:

- A recipe can contain many ingredients. These ingredients usually have a name, a quantity, and a unit of measurement. Each ingredient for a recipe would require its own row, which should allow for modification (i.e. update or delete) at a later stage.
- A recipe can also contain many steps. These steps are usually sequential, hence have a number associated with them. Therefore, each step would also require its own row, to allow for modification at a later stage.
- Filtering would be done at code level, and not database level. However, since multiple recipes may have the same property such as a dietary tag or ingredient, the indexes would need to be carefully chosen to allow for performant querying of the database.

Based on the above assumptions, the database was normalised (3NF). Together with the ID (by default), the foreign key (RID) was also chosen as an index to improve performance (such as for the underlying JOIN, DELETE and UPDATE operations).

## API Design

### Security and Monitoring
To ensure that the API is secure, and to prevent unknown parties from being able to access the various endpoints, authentication was incorporated. This was achieved using OAuth 2.0 with JWT (with Auth0 as the identity provider). Save for the `GET /api/recipes` endpoint, the remaining endpoints were protected.

For monitoring, to ensure that the API would provide information on events and errors, logging was incorporated. The logs were also enhanced to return a format compatible with ElasticSearch, thus ensuring a seamless integration with this service if monitoring were to be included.


### Available Endpoints
In choosing the functionality to surface to a client, it was decided that the following endpoints would be made available: <br>

`GET /api/recipes` - Get all recipes<br>
`GET /api/recipes/{id}` - Get recipe by ID <br>
`GET /api/recipes/title` - Get recipe by title <br>
`GET /api/recipes/dietaryTag` - Get recipes by dietary tag <br>

`POST api/recipes/add` - Add new recipe <br>

`PATCH  /api/recipes/update/{id}` - Update recipe by ID <br>
`PATCH  /api/recipes/update/title` - Update recipe by title <br>

`DELETE /api/recipes/{id}` - Delete recipe by ID <br>


## API Structure

With SOLID in mind, it was decided to use a controller-based API. A quick look at the repository shows that all the components can be easily changed or subsituted without having a significant impact on its functionality.



## Tradeoffs
- Using EntityFramework vs Raw SQL
    - The use of an ORM vs raw SQL  could mean that as queries increase in complexity, the ability to debug easily lessens. However, in using an ORM, SQL injection, manually creating the database schema and defining relationships, as well as writing complex SQL scripts are avoided.
- Granular vs generic filtering for endpoints
    - The endpoints were chosen based on the envisioned interaction of a client with the API. Rather than create a generic endpoint that accepted any filter, the the endpoints were designed to cater for specific filters, thus ensuring separation of concerns and easier maintainability of the codebase.
