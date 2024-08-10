FROM node:20 AS build
WORKDIR /app

# Copy package.json and package-lock.json
COPY package*.json ./

# Install dependencies
RUN npm ci

# Copy the rest of the code
COPY . .

# Build the application
RUN npm run build -- --configuration production

# Serve the app with Nginx
FROM nginx:alpine
COPY --from=build /app/dist/quote-request-system/browser /usr/share/nginx/html
COPY nginx.conf /etc/nginx/conf.d/default.conf