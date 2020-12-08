# Coding Test

## Setting up the credentials

Since this API is dependent on the Spotify API, you must first set up the correct credentials in order for this application to be able to reach the Spotify API.

Change directory to the CodingTestApi directory and execute the following dotnet CLI commands:

1:

```
dotnet user-secrets set "Spotify:ClientId" "<CLIENT_ID>"
```

2:

```
dotnet user-secrets set "Spotify:ClientSecret" "<CLIENT_SECRET>"
```

Where `<CLIENT_ID>` and `<CLIENT_SECRET>` are your access credentials for the Spotify API.

## Running the API

### Prerequisites

* .NET 5 installed

### Run via dotnet CLI

Change directory to the CodingTestApi directory and execute the following dotnet CLI command:

```
dotnet run --launch-profile CodingTestApi
```

## Testing the API

### Manual tests

As soon as the API is up and running, manual tests can be performed.

#### Swagger

Open a browser and visit: `https://localhost:5001/swagger`

The GUI is pretty intuitive, so knock yourself out :v:

### Curl

If you have access to curl on your computer you may test using the follwing command template:

```
curl -X GET "https://localhost:5001/api/spotify/artist?artistName=<ARTIST_NAME_QUERY_STR>" -H  "accept: application/json"
```

Where `<ARTIST_NAME_QUERY_STR>` is replaced with the desired query string parameter. Please have in mind that the query string parameter __must__ be URL encoded.

### Unit tests

Change directory to the CodingTestApiTests directory and execute the following dotnet CLI command:

```
dotnet test
```