// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function onSearchLoad() {
	const formLogin = document.getElementById("Search");
	


	formLogin.addEventListener("submit", async function (event) {
		event.preventDefault();
		const encoder = new TextEncoder();
		const searchBox = formLogin.getElementsByClassName("SearchBox")[0].value;
		if (searchBox != "") {
			AJAXfunc(searchBox);
		}
		else {
			alert("please enter a valid search term")
		}
	});

}


function AJAXfunc(Search) {
	var parameter = JSON.stringify({ "Search": Search })

	/*
	$.ajax({
		type: "POST",
		contentType: 'application/json; charset=utf-8',
		url: 'api/SearchFunc',
		data: parameter,
		dataType: 'json',
		headers: { "RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val() },

		success: function (data) {
			onsuccess(data)
			//return data
		},
		error: function (data, success, error) {
			alert("Error: " + error + " - " + data + " - " + success + " - " + data.value)
		}
	})
	*/
	let tmp;
	fetch("api/SearchFunc", {
		method: "POST",
		headers: {
			"RequestVerificationToken": $('input[name="__RequestVerificationToken"]').val(),
			'Accept': 'application/json',
			'Content-Type': 'application/json'
		},
		body: parameter

	}).then(response => response.json()).then(data => onsuccess(data));
}
function onsuccess(data) {
	const BookDisplay = document.getElementById("BookDisplay")

	let i = 0
	while (BookDisplay.firstChild) {
		BookDisplay.firstChild.remove();
	}
	for (i = 0; i < data.docs.length; i++) {

		let container = document.createElement('div');

		let element = document.createElement('h3');
		element.textContent = data.docs[i].title;
		container.appendChild(element);


		let Author = document.createElement('small');
		Author.textContent = data.docs[i].author_name;
		container.appendChild(Author);

		BookDisplay.appendChild(container);

		let Image = document.createElement('img');
		//Image.src = "data:image/png;base64," + data.docs[i].Cover; //if using a byte string from server
		Image.src = data.docs[i].cover;
		BookDisplay.appendChild(Image);

	}
}
