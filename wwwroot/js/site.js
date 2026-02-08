
document.addEventListener('DOMContentLoaded', function () {
    const uploadForm = document.getElementById('uploadForm');
    const documentsTable = document.getElementById('documentsTable').getElementsByTagName('tbody')[0];

    // Fetch and display documents
    async function getDocuments() {
        const response = await fetch('/api/documents');
        const documents = await response.json();

        documentsTable.innerHTML = '';

        documents.forEach(doc => {
            let row = documentsTable.insertRow();

            let idCell = row.insertCell(0);
            idCell.textContent = doc.id;

            let nameCell = row.insertCell(1);
            nameCell.textContent = doc.name;

            let actionsCell = row.insertCell(2);
            let deleteButton = document.createElement('button');
            deleteButton.textContent = 'Delete';
            deleteButton.onclick = async () => {
                await fetch(`/api/documents/${doc.id}`, { method: 'DELETE' });
                getDocuments();
            };
            actionsCell.appendChild(deleteButton);
        });
    }

    // Handle file upload
    uploadForm.addEventListener('submit', async function (e) {
        e.preventDefault();

        const fileInput = document.getElementById('file');
        const formData = new FormData();
        formData.append('file', fileInput.files[0]);

        await fetch('/api/documents', {
            method: 'POST',
            body: formData
        });

        fileInput.value = '';
        getDocuments();
    });

    getDocuments();
});
