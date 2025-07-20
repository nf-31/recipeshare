# Introduction

This document contains all notes and information related to the architecture and design of this solution.

## Database Design

Below is an Entity Relationship Diagram for the database used in the solution.

![Database ER Diagram](/documentation/database-er.png)

The following assumptions were made for the database design:

- A recipe can contain many ingredients. These ingredients usually have a name, a quantity, and a unit of measurement. Each ingredient for a recipe would require its own row, which should allow for modification (i.e. update or delete) at a later stage.
- A recipe can also contain many steps. These steps are usually sequential, hence have a number associated with them. Therefore, each step would also require its own row, to allow for modification at a later stage.
- Filtering would be done at code level, and not database level. However, since multiple recipes may have the same property such as a dietary tag or ingredient, the indexes would need to be carefully chosen to allow for performant querying of the database.