function get(endpoint, name) {
    return fetch(process.env.REACT_APP_API_URI + endpoint, {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json',
          'name': name
        }
      })
      .then(response => response.json());
}

function post(endpoint, name, content) {
    return fetch(process.env.REACT_APP_API_URI + endpoint, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          'name': name
        },
        body: JSON.stringify(content)
      })
      .then(response => response.json());
}

export {get, post};