import React, { useState } from 'react';
import {post} from '../helpers/ApiClient';

function PlayCpu({name, choiceOptions}) {
  const [response, setResponse] = useState("");

  const onChoice = (playerChoice) => {
    setResponse("");
    post('/play', name, {player: playerChoice})
    .then(data => {
      setResponse(data.results)
    })
  };

  return (
    <div>
        <p>Play against the CPU</p>
        <p>{response}</p>
        <div>
          {choiceOptions.map(x => <button key={x.id} onClick={() => {onChoice(x.id)}}>{x.name}</button>)}
        </div>
    </div>
  );
}

export default PlayCpu;
