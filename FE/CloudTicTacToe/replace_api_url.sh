#!/usr/bin/env sh

find '/app' -name '*.ts' -exec sed -i -e 's,API_BASE_URL,'"$API_BASE_URL"',g' {} \;
ng serve --host=0.0.0.0 --configuration=production
