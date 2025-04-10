# STIX API

This is a REST API for creating, updating, deleting, and retrieving STIX2 Vulnerability objects. The API is built using ASP.NET Core and follows the STIX2 specification.

## Prerequisites

This repository is using devcontainer for development. You will need to install the following:

* [Docker](https://www.docker.com/get-started)
* [Visual Studio Code](https://code.visualstudio.com/)
* [Remote - Containers extension](https://marketplace.visualstudio.com/items?itemName=ms-vscode-remote.remote-containers)

## Getting Started

Use the Makefile to build and run the application, just run the `make` in the terminal to see all available rules.

## Resources

* STIX model - <https://oasis-open.github.io/cti-documentation/stix/intro.html>
    * Vulnerability model - <https://docs.oasis-open.org/cti/stix/v2.1/os/stix-v2.1-os.html#_q5ytzmajn6re>
    * JSON Schema for Vulnerability can be found here: <https://github.com/oasis-open/cti-stix2-json-schemas/blob/master/schemas/sdos/vulnerability.json>
* Vulnerability examples - <https://github.com/oasis-open/cti-stix-common-objects/tree/main/objects/vulnerability>

## curl commands

### Create STIX2 Vulnerability object

Token can be generated using the following command:

```bash
make create-jwt
```

Then use the token in the following command:

```bash
curl -k -H "Authorization: Bearer {token}" -X POST \
  https://localhost:7195/v1/api/vulnerabilities \
  -H 'Content-Type: application/json' \
  -d @./docs/schemas/sdos/examples/vulnerability.json 
```

### Get STIX2 Vulnerability object

```bash
curl -k -X GET \
  https://localhost:7195/v1/api/vulnerabilities/vulnerability--e9eb06c9-ebc1-47a6-a009-4702bd9f744a \
  -H 'Content-Type: application/json'
```

### Get all STIX2 Vulnerability objects

```bash
curl -k -X GET \
  https://localhost:7195/v1/api/vulnerabilities \
  -H 'Content-Type: application/json'
```

### Delete STIX2 Vulnerability object

Token can be generated using the following command:

```bash
make create-jwt-admin
```

Then use the token in the following command:

```bash
curl -k -H "Authorization: Bearer {token}" -X DELETE \
  https://localhost:7195/v1/api/vulnerabilities/vulnerability--e9eb06c9-ebc1-47a6-a009-4702bd9f744a \
  -H 'Content-Type: application/json'
```

### Update STIX2 Vulnerability object

Token can be generated using the following command:

```bash
make create-jwt
```

Then use the token in the following command:

```bash
curl -k -H "Authorization: Bearer {token}" -X PUT \
  https://localhost:7195/v1/api/vulnerabilities/vulnerability--e9eb06c9-ebc1-47a6-a009-4702bd9f744a \
  -H 'Content-Type: application/json' \
  -d @./docs/schemas/sdos/examples/update_vulnerability.json 