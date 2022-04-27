FROM node:lts-alpine
WORKDIR /usr/src/app
COPY ["package*.json", "npm-shrinkwrap.json*", "./"]
RUN npm config rm proxy && npm config rm https-proxy
RUN npm install
COPY . .
EXPOSE 3000
CMD ["npm", "start"]