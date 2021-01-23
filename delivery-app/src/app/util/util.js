export const findCoordinatesByAddress = async (address) => {
    console.log(address)
    const url = `https://api.tomtom.com/search/2/geocode/${encodeURIComponent(address)}.json?limit=1&countrySet=PL&key=${process.env.REACT_APP_TOM_TOM_API_KEY}`
    return fetch(url, {
        method: "GET",
    })
    .then(response => response.json())
    .catch(e => {
        console.error("error", e)
    })
}