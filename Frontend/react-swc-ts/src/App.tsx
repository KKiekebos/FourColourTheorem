import { useEffect, useState } from 'react'
import './App.css'
import type { Colouring } from './Colouring';

const postBody = {
  adjacencyMatrix: [
    [0, 1, 1, 1, 0],
    [1, 0, 1, 0, 0],
    [1, 1, 0, 1, 0],
    [1, 0, 1, 0, 1],
    [0, 0, 0, 1, 0]
  ]
};

function App() {
  const [colourData, setColourData] = useState<Colouring[]>();
  const [errorMessage, setErrorMessage] = useState<string>("");

  const fetchData = async () => {
    try {
      const response = await fetch('https://localhost:7168/FourColourTheorem', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(postBody)
      });


      if (response.ok) {
        const jsonResponse: Colouring[] = await response.json();
        console.log(jsonResponse)
        setColourData(jsonResponse)
      } else {
        const error = await response.json()
        setErrorMessage(error.message || "something went wrong getting colours for your map")
      }
    } catch {
      setErrorMessage("something went wrong getting colours for your map")
    }
  };

  useEffect(() => {
    fetchData()
  }, [])

  return (
    <div>
      <div className='colour0'>Colour 1</div>
      <div className='colour1'>Colour 2</div>
      <div className='colour2'>Colour 3</div>
      <div className='colour3'>Colour 4</div>
      <div className='svgParent'>
        <ul>
          { // I planned to draw some svg arcs underneath, 
            // showing neighbouring regions.
            // I have generated svgs with code before, 
            // but lacked the time to do so for this projects 
            colourData && colourData.map(function (d, idx) {
              return (<li key={idx} className={`regionList colour${d.colour}`}>Region {d.regionId}</li>)
            })}
        </ul>
      </div>
      {errorMessage !== "" && errorMessage && <p className='error'>Error: {errorMessage}</p>}
    </div>

  )
}

export default App
