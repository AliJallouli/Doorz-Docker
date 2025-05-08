FROM node:20-alpine
WORKDIR /app
COPY package*.json ./
RUN npm install -g @angular/cli
RUN npm install --legacy-peer-deps
COPY . .
EXPOSE 4200
CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "4200", "--ssl", "true", "--ssl-cert", "localhost.pem", "--ssl-key", "localhost-key.pem"]