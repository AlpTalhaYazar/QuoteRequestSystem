# Quote Request System Frontend

This is the frontend for the Quote Request System, built with Angular 18 and NG Zorro.

## Project Structure

```
frontend/
└── src/
    └── quote-request-system/
        ├── src/
        │   ├── app/
        │   │   ├── components/
        │   │   ├── guards/
        │   │   ├── interceptors/
        │   │   ├── models/
        │   │   └── services/
        │   ├── assets/
        │   └── environments/
        ├── angular.json
        └── package.json
```

## Prerequisites

- Node.js (v14 or later)
- npm (v6 or later)

## Setup

1. Clone the repository
2. Navigate to the `frontend/src/quote-request-system` directory
3. Run the following commands:

```
npm install
ng serve
```

The application will be available at `http://localhost:4200`.

## Building for Production

To build the application for production, run:

```
ng build --prod
```

The build artifacts will be stored in the `dist/` directory.

## Running Tests

To execute the unit tests via Karma, run:

```
ng test
```

## Further Help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI Overview and Command Reference](https://angular.io/cli) page.