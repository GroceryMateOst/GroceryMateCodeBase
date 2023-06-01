# Multi-stage
# 1) Node image for building frontend assets
# 2) nginx stage to serve frontend assets

FROM node:18-alpine AS builder

WORKDIR /app

COPY . .

RUN yarn install && yarn build

# nginx state for serving content
FROM nginx:alpine

WORKDIR /usr/share/nginx/html

RUN rm -rf ./*

COPY --from=builder /app/dist .

ENTRYPOINT ["nginx", "-g", "daemon off;"]