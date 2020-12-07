# Coding Test

## Setting up the credentials

Change directory to the CodingTestApi directory and execute the following dotnet CLI commands:

1:

```
dotnet user-secrets set "Spotify:ClientId" "<CLIENT_ID>"
```

2:

```
dotnet user-secrets set "Spotify:ClientSecret" "<CLIENT_SECRET>"
```

## Running the API

Change directory to the CodingTestApi directory and execute the following dotnet CLI command:

```
dotnet run --launch-profile CodingTestApi
```