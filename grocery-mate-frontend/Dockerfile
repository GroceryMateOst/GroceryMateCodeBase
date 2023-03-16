FROM node:18-alpine

EXPOSE 3000

WORKDIR /frontend

ARG BACKEND_URL                    
ENV BACKEND_URL =$BACKEND_URL   

COPY package.json .

RUN yarn install

COPY . .

CMD [ "yarn","dev"]