import React, { useState } from 'react';
import {get} from '../helpers/ApiClient';
import PlayCpu from './PlayCpu';
import PlayMulti from './PlayMulti';
import LatestPlays from './LatestPlays';
import YourHistory from './YourHistory';

function TabbedContent({name}) {
  const [page, setPage] = useState("cpu");
  const [choiceOptions, setChoiceOptions] = useState([]);

  React.useEffect(() => {
    get("/choices")
    .then(data => {
      setChoiceOptions(data);
    })
  }, [])

  return (
    <div>
      <button onClick={() => setPage("cpu")}>CPU</button>
      <button onClick={() => setPage("multi")}>Multiplayer</button>
      <button onClick={() => setPage("latest")}>Latest</button>
      <button onClick={() => setPage("history")}>Your history</button>
      {page === 'cpu' && <PlayCpu name={name} choiceOptions={choiceOptions}/>}
      {page === 'multi' && <PlayMulti name={name} choiceOptions={choiceOptions}/>}
      {page === 'latest' && <LatestPlays />}
      {page === 'history' && <YourHistory name={name} />}
    </div>
  );
}

export default TabbedContent;
