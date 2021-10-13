

$(() => {
   LoadProdData();
    var connection = new signalR.HubConnectionBuilder().withUrl("/signalrServer").build();

    connection.start();
    connection.on("LoadEmployees", function () {
        LoadProdData();
    })

    LoadProdData();     


    function LoadProdData() {
        var tr = '';

        $.ajax ({
            url: '/Employees/GetEmployees',
                method: 'GET',
                success: (result) => {
                    $.each(result, (k, v) => {
                        tr += `<tr>
                                <td>${v.EmpName}</td>
                                <td>${v.Salary}</td >
                                
                                <td>${v.DeptName}</td>
                                <td>${v.Designation}</td>                               
                                <td>
                                <a href='../Employees/Edit?id=${v.EmpId}'>Edit</a>
                                <a href='../Employees/Details?id=${v.EmpId}'>Details</a>
                                <a href='../Employees/Delete?id=${v.EmpId}'>Delete</a>
                                </td>
                               <tr>`
                        })
                       $("#tableBody").html(tr);
                },
            error: (error) => { console.log(error) }
         });
    }
 
   
})
