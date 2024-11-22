FROM node:18 AS build
WORKDIR /app

# Copy package.json and install dependencies
COPY package*.json ./
RUN npm install

# Copy the rest of the application code
COPY . ./

# Build the app for production
RUN npm run build

# Use an Nginx image to serve the built app
FROM nginx:1.25
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]