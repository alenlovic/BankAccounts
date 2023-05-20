FROM node:latest as builder

COPY src /src

WORKDIR /src

RUN npm install
RUN $(npm bin)/ng build 

COPY . .

ENV PORT=8080

EXPOSE 8080

CMD [ "npm", "start" ]