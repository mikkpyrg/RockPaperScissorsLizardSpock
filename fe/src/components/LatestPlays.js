import React, { useState } from 'react';
import {get} from '../helpers/ApiClient';

function LatestPlays() {
  const [list, setList] = useState([]);

  React.useEffect(() => {
    get('/latest')
    .then(data => {
      setList(data)
    })
  }, [])

  return (
    <div>
        <p>Latest 10 plays</p>
        <div>
          {list.map((x, i) => <p key={i}>{`${x.playerChoice}(${x.playerName}), ${x.challengerChoice}(${x.challengerName}): ${x.result}`}</p>)}
        </div>
    </div>
  );
}

export default LatestPlays;
