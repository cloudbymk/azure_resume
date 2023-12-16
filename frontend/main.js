window.addEventListener('DOMContentLoaded', (event) =>
{
    getVisitCount();
})

const functionApiUrl = "https://getresumecountermk.azurewebsites.net/api/GetAndUpdateCounter?code=df8vkRB3wfduSpNESfXxKuM3RsZkRXMOwrNZBgMh41UzAzFunEzN7w==";
const functionApi = 'http://localhost:7071/api/GetAndUpdateCounter';

const getVisitCount = () =>
{

        let count = 10;
        fetch(functionApiUrl).then(Response =>
            {
                return Response.json()

            }).then(response =>
            {
                console.log("Website called API function");
                count = response.count;
                document.getElementById("counter").innerText = count;
            }).catch(function(error)
            {
                console.log(error);
            });
            return count;
}