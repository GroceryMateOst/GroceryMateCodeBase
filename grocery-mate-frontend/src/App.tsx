import { useEffect, useState } from 'react'
import reactLogo from './assets/react.svg'
import './App.css'
import { ClickService } from './services/click-serbice'

function App() {
  const [count, setCount] = useState(0)

  useEffect(() => {
    const clickService = new ClickService();
    const fetchData =async () => {
      return await clickService.getClicks()
    }
    fetchData().then((e)=>setCount(e.data.numberOfClicks))
  }, [])
  

  const countUp = async () =>{
    const clickService = new ClickService();
    const newCount = count+1
    await clickService.sendClicks(newCount);
    setCount(newCount)
  }

  return (
    <div className="App">
      <div>
        <a href="https://vitejs.dev" target="_blank">
          <img src="/vite.svg" className="logo" alt="Vite logo" />
        </a>
        <a href="https://reactjs.org" target="_blank">
          <img src={reactLogo} className="logo react" alt="React logo" />
        </a>
      </div>
      <h1>Vite + React</h1>
      <div className="card">
        <button onClick={countUp}>
          count is {count}
        </button>
        <p>
          Edit <code>src/App.tsx</code> and save to test HMR
        </p>
      </div>
      <p className="read-the-docs">
        Click on the Vite and React logos to learn more
      </p>
    </div>
  )
}

export default App
