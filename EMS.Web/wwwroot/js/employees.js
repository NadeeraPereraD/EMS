let editModal;
let deleteModal;

document.addEventListener("DOMContentLoaded", () => {
    editModal = new bootstrap.Modal(document.getElementById("editEmployeeModal"));
    deleteModal = new bootstrap.Modal(document.getElementById("deleteEmployeeModal"));
    loadEmployees();

    document.getElementById("editEmployeeForm").addEventListener("submit", async function (e) {
        e.preventDefault();

        const Id = parseInt(document.getElementById("editId").value);
        const name = document.getElementById("editName").value;
        const email = document.getElementById("editEmail").value;
        const jobPosition = document.getElementById("editJP").value;

        const employee = {Id, name, email, jobPosition};

        try
        {
            const response = await fetch("https://localhost:7192/api/employees/by-key", {
            method: "PUT",
            headers: {"Content-Type": "application/json" },
            body: JSON.stringify(employee)
                        });

            if (response.ok) {
                alert("Employee updated successfully.");
            editModal.hide();
            loadEmployees(); // Refresh the table
                    } else {
                        const error = await response.text();
            alert("Failed to update employee: " + error);
                    }
        }
        catch (error)
        {
            alert("Error updating employee: " + error.message);
        }
        });



    document.getElementById("employeeForm").addEventListener("submit", async function (e) {
        e.preventDefault();

        const name = document.getElementById("inputName").value;
        const email = document.getElementById("inputEmail").value;
        const jobPosition = document.getElementById("inputJP").value;

        const employee = {name, email, jobPosition};

        try
        {
            const response = await fetch("https://localhost:7192/api/employees", {
            method: "POST",
            headers: {
            "Content-Type": "application/json"
                        },
            body: JSON.stringify(employee)
                    });

            if (response.ok) {
                alert("Employee added successfully!");
            document.getElementById("employeeForm").reset();
            loadEmployees();
                    } else {
                        const error = await response.text();
            alert("Failed to add employee: " + error);
                    }
        }
        catch (error)
        {
            alert("Error: " + error.message);
        }
    });
});

async function loadEmployees() {
    try {
        const response = await fetch("https://localhost:7192/api/employees");
        if (!response.ok) throw new Error("Failed to fetch employees");

        const result = await response.json();
        const employees = result.data;
        const tableBody = document.getElementById("employeeTableBody");
        tableBody.innerHTML = "";

        employees.forEach(emp => {
        const row = document.createElement("tr");
        row.innerHTML = `
            <td>${emp.name}</td>
            <td>${emp.email}</td>
            <td>${emp.jobPosition}</td>
            <td>
                <button class="btn btn-sm btn-outline-success" onclick="updateEmployee('${emp.id}')">
                    <i class="fas fa-edit"></i>
                </button>
                &nbsp;
                <button class="btn btn-sm btn-outline-danger" onclick="deleteEmployee('${emp.id}')">
                    <i class="fas fa-trash"></i>
                </button>
            </td>`;
            tableBody.appendChild(row);
        });
    }
    catch (error)
    {
        alert("Error loading employees: " + error.message);
    }
}

async function updateEmployee(id) {
    try {
        const response = await fetch("https://localhost:7192/api/employees/by-key", {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ id })
        });

        if (!response.ok) {
            alert("Failed to load employee data.");
            return;
        }

        const result = await response.json();
        const emp = result.data;

        document.getElementById("editId").value = emp.id;
        document.getElementById("editName").value = emp.name;
        document.getElementById("editEmail").value = emp.email;
        document.getElementById("editJP").value = emp.jobPosition;

        editModal.show();
    }
    catch (error)
    {
        alert("Error loading employee data: " + error.message);
    }
}

async function confirmDelete() {
    const id = parseInt(document.getElementById("deleteId").value);
    try {
        const response = await fetch("https://localhost:7192/api/employees/soft-delete-by-key", {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ id })
        });

        if (response.ok) {
            alert("Employee deleted successfully.");
            deleteModal.hide();
            loadEmployees();
        } else {
            const error = await response.text();
            alert("Failed to delete employee: " + error);
        }
    }
    catch (error)
    {
        alert("Error deleting employee: " + error.message);
    }
}

function deleteEmployee(id) {
    fetch("https://localhost:7192/api/employees/by-key", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ id: parseInt(id) })
    })
    .then(async res => {
        if (!res.ok) {
            const errText = await res.text();
            throw new Error("Server Error: " + errText);
        }
        const text = await res.text();
        if (!text) {
            throw new Error("Empty response from server.");
        }
        const result = JSON.parse(text);
        if (!result.data) {
            throw new Error("Missing 'data' in response.");
        }
        const emp = result.data;
        document.getElementById("deleteId").value = emp.id;
        document.getElementById("deleteNameText").innerText = emp.name;
        deleteModal.show();
    })
    .catch(err => {
        alert("Failed to fetch employee data for delete: " + err.message);
    });
}
