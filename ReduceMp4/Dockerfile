FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /App
COPY --from=build-env /App/out .

RUN apt-get -y update
RUN apt-get install -y ffmpeg

RUN mkdir /usr/share/reducemp4 \
	mkdir /usr/share/reducemp4/output

VOLUME /usr/share/reducemp4

ENTRYPOINT ["dotnet", "ReduceMp4.dll"]
