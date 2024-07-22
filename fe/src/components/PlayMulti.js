import React, { useState } from 'react';
import {post} from '../helpers/ApiClient';

function PlayMulti({name, choiceOptions}) {
  const [response, setResponse] = useState("");

  const onChoice = (playerChoice) => {
    setResponse("");
    post('/play-multiplayer', name, {player: playerChoice})
    .then(data => {
      if (data.result) {
        setResponse(`You: ${data.playerChoice}, challenger: ${data.challengerChoice}, result: ${data.result}`);
      } else {
        setResponse("Waiting for a challenger, check your history for results");
      }
    })
  };

  return (
    <div>
        <p>Play against another player</p>
        <p>{response}</p>
        <div>
          {choiceOptions.map(x => <button key={x.id} onClick={() => {onChoice(x.id)}}>{x.name}</button>)}
        </div>
    </div>
  );
}

export default PlayMulti;
