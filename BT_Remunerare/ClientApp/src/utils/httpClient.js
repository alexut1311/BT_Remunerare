const backEndURL = "https://localhost:7165/api";

/**
 * Perform a GET operation
 * @param {string} url relative to the host name
 */
const httpGet = (url) => {
  return fetch(`${backEndURL}${url}`);
};

/**
 * Perform a POST operation
 * @param {string} url relative to the host name
 * @param {object} content the data to send
 */
const httpPost = (url, content = {}) => {
  return fetch(`${backEndURL}${url}`, {
    method: "POST", // GET, *POST, PUT, DELETE, etc.
    headers: {
      "Content-Type": "application/json", // *application/json, application/x-www-form-urlencoded
    },
    body: JSON.stringify(content),
  });
};

/**
 * Perform a DELETE operation
 * @param {string} url relative to the host name
 * @param {object} id the id to send
 */
const httpDelete = (url, id = 0) => {
  return fetch(`${backEndURL}${url}/${id}`, {
    method: "DELETE", // GET, *POST, PUT, DELETE, etc.
    headers: {
      "Content-Type": "application/json", // *application/json, application/x-www-form-urlencoded
    },
  });
};

export default {
  get: httpGet,
  post: httpPost,
  delete: httpDelete,
};
