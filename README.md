# Pay Gen
![Pay Gen](images/screenshot-home.PNG)

PayGen is .NET App that generates Employee's monthly pay slip. This is an assignment for Datacom recruitment.

### Architecture

PayGen is built with the idea of Domain Driven Design. We try to separate the complicated business logic in domain models (Aggregate, Entities, Value Object) and publish domain events for integration among other bounded contexts.
![Domain Driven Design](images/ddd-patterns.png)

### Project Structure

## Installation
- Checkout main branch
- Open the `PayGen.sln` solution .
- Build

## Configuration
- The Tax Slab config is set to use default (as suggested in problem statement). If needed to change, set Tax Slab configuration in `appsettings.json` file accordingly
```javascript
"TaxSlab": [
    {
      "StartRange": 0,
      "EndRange": 14000,
      "Rate": 0.105
    },
    {
      "StartRange": 14000,
      "EndRange": 48000,
      "Rate": 0.175
    },
    ...
  ],	
```javascript

## Usage
Run using IIS or IISExpress (hit ctrl + F5). It will open PayGen User Interface.


## Assumptions
