import React, { useState } from 'react';
import {get} from '../helpers/ApiClient';

function YourHistory({name}) {
  const [list, setList] = useState([]);

  React.useEffect(() => {
    get('/history', name)
    .then(data => {
      setList(data.map(item => {
        if (item.result) {
          return `${item.playerChoice} vs ${item.challengerChoice}(${item.challengerName}): ${item.result}`;
        } else {
          return `${item.playerChoice} vs {awaiting challenger}`;
        }
      }))
    })
  }, [name])

  return (
    <div>
        <p>Your plays, latest to newest</p>
        <div>
          {list.map((x, i) => <p key={i}>{x}</p>)}
        </div>
    </div>
  );
}

export default YourHistory;
