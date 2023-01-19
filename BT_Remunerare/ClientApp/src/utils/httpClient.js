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
  const requestOptions = {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(content),
  };

  return fetch(`${backEndURL}${url}`, requestOptions);
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
