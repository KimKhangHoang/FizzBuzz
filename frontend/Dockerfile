# Use a Node.js base image
FROM node:18 AS build

# Set the working directory
WORKDIR /app

# Copy package.json and package-lock.json into the working directory
COPY ./frontend/package.json ./frontend/package-lock.json ./

# Install dependencies
RUN npm install && npm cache clean --force

# Copy the rest of your application code
COPY frontend/ .

# Build the application using TypeScript and Vite
RUN npm run build

# Serve the app using a lightweight server (like Nginx)
FROM nginx:alpine

# Copy build files from the previous stage
COPY --from=build /app/dist /usr/share/nginx/html

# Expose the port the app runs on
EXPOSE 80

# Command to run the server
CMD ["nginx", "-g", "daemon off;"]