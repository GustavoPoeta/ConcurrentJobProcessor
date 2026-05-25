
import {
    HubConnectionBuilder
} from "https://esm.sh/@microsoft/signalr";

const productsDiv = document.getElementById("productsDiv");
const statusDiv = document.getElementById("connectionStatus");

const connection = new HubConnectionBuilder()
    .withUrl("http://localhost:5000/products/hub", {
        withCredentials: true
    })
    .withAutomaticReconnect()
    .build();

function stringToColor(str) {

    let hash = 0;

    for (let i = 0; i < str.length; i++) {
        hash = str.charCodeAt(i) + ((hash << 5) - hash);
    }

    const colors = [
        "#3483fa",
        "#00a650",
        "#ff7733",
        "#7b61ff",
        "#ff5a5f",
        "#00bcd4",
        "#ffb300",
        "#8bc34a",
        "#795548",
        "#607d8b"
    ];

    return colors[Math.abs(hash) % colors.length];
}

function renderProducts(products) {

    productsDiv.innerHTML = "";

    if (!products || products.length === 0) {
        productsDiv.innerHTML = `
                    <div class="empty-state">
                        No products received.
                    </div>
                `;
        return;
    }

    products.forEach(product => {

        const categoriesHtml = product.categories
            .map(category => `
                        <span class="category-tag">
                            ${category}
                        </span>
                    `)
            .join("");

        const productCard = `
                    <div class="product-card">

                        <div class="product-image"
                        style="background:${stringToColor(product.name)}">>
                        </div>

                        <div class="product-content">

                            <div class="product-name">
                                ${product.name}
                            </div>

                            <div class="categories">
                                ${categoriesHtml}
                            </div>

                            <div class="price">
                                $${product.price.toFixed(2)}
                            </div>

                        </div>

                    </div>
                `;

        productsDiv.innerHTML += productCard;
    });
}

connection.on("ProductsUpdated", (updatedProducts) => {

    console.log("Products received:", updatedProducts);

    renderProducts(updatedProducts);
});

connection.onreconnected(() => {
    statusDiv.innerText = "Connected";
    statusDiv.className = "status connected";
});

connection.onclose(() => {
    statusDiv.innerText = "Disconnected";
    statusDiv.className = "status disconnected";
});

async function start() {

    try {

        await connection.start();

        console.log("SignalR Connected.");

        statusDiv.innerText = "Connected";
        statusDiv.className = "status connected";

    } catch (err) {

        console.error(err);

        statusDiv.innerText = "Failed to connect";
        statusDiv.className = "status disconnected";

        setTimeout(start, 5000);
    }
}

start();

async function handleFormSubmit(event) {

    event.preventDefault();

    const fileInput = document.getElementById("fileInput");
    const file = fileInput.files[0];

    if (!file) {
        alert("Please select a file.");
        return;
    }

    const formData = new FormData();

    formData.append("file", file);

    try {

        const response = await fetch(
            "http://localhost:5000/api/products/upload-csv",
            {
                method: "POST",
                body: formData
            }
        );

        if (response.ok) {

            console.log("Task submitted successfully.");

        } else {

            console.error("Failed to submit task.");
        }

    } catch (error) {

        console.error(error);
    }
}

document
    .getElementById("myForm")
    .addEventListener("submit", handleFormSubmit);
