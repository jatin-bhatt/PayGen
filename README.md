# Pay Gen
![Pay Gen](images/screenshot.PNG)

PayGen is .NET Core App that generates monthly pay slip for Employee. It is an assignment for Datacom recruitment

### Architecture

PayGen is built with following Domain Driven Design. We try to keep the complicated business logic in domain models (Aggregate, Entities, Value Object) and publish domain events for integration among other bounded context.
![Domain Driven Design](images/ddd-patterns.png)

## Installation
- Checkout main branch
- Open the `PayGen.sln` solution .

- Build
- Run migrations

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
	```

## Usage
Run using IIS or IISExpress (hit ctrl + F5). It will open PayGen UI interface.