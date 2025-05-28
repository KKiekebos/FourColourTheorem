# Four Colour Theorem - map colourer
## A small web api that colours a flat map with as few colours as possible
This small program takes an adjacency matrix that indicates whether regions share a border, and will assign colours to each region. It will assign as few different colours as possible, so if two colours are sufficient, it will not use more. 

## How to run
The web api can be tested using swagger. An example input is provided at the end of this readme.

The frontend can be run using `npm run dev`. The backend needs to be up and running before the frontend will show the regions and their colours. 

The matrix is provided in App.tsx and can be adjusted there.

## Tests
There are a few unit tests and two integration tests that check if the functionality works as expected.

## Example input
Use this input as an example when running the program:
```
{
  "adjacencyMatrix": [
    [
      0, 1, 1, 1, 0
    ],
    [
      1, 0, 1, 0, 0
    ],
    [
      1, 1, 0, 1, 0
    ],
    [
      1, 0, 1, 0, 1
    ],
    [
      0, 0, 0, 1, 0
    ]
  ]
}
```
