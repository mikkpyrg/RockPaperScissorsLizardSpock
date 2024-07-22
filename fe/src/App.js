import './App.css';
import React, { useState } from 'react';
import TabbedContent from './components/TabbedContent';

function App() {
  const [name, setName] = useState("");
  const [internalName, setInternalName] = useState("");

  return (
    <div className="App">
      {!name 
        ? <div>
            <input type="text" value={internalName} onChange={(e) => setInternalName(e.target.value)} placeholder='Name' />
            <button disabled={!internalName} onClick={() => {setName(internalName)}}>Set name</button>
          </div> 
        : <TabbedContent name={name}/>
      }
    </div>
  );
}

export default App;
