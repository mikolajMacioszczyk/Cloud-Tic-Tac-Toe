# cd \FE\CloudTicTacToe\
# docker build -t macioszczykmikolaj/tictactoe-web .
# docker push macioszczykmikolaj/tictactoe-web

FROM --platform=$BUILDPLATFORM node:20.8.0-bullseye-slim as builder

WORKDIR /app

RUN npm install -g @angular/cli@17

COPY package.json package-lock.json ./

RUN npm ci

COPY . .
EXPOSE 80

#CMD ["sh", "./replace_api_url.sh"]
CMD ["ng", "serve", "--host", "0.0.0.0", "--port", "80", "--configuration", "production"]