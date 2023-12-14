window.addEventListener('DOMContentLoaded', (event) =>
{
    getVisitCount();
})


const functionApi = '';

const getVisitCount = () =>
{

        let count = 10;
        fetch(functionApi).then(Response =>
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