document.addEventListener("DOMContentLoaded", () => {
    const gallery = document.getElementById("gallery");
    const eventsContainer = document.getElementById("events-container");

    fetch("/api/Celebrities")
        .then(response => {
            if (!response.ok) throw new Error("Ошибка загрузки данных");
            return response.json();
        })
        .then(celebrities => {
            celebrities.forEach(celebrity => {
                if (celebrity.reqPhotoPath) {
                    const img = document.createElement("img");

                    img.src = `/api/Celebrities/photo/${celebrity.reqPhotoPath}`;
                    img.alt = celebrity.fullName;

                    img.style.height = "150px";
                    img.style.cursor = "pointer";
                    img.style.objectFit = "cover";

                    img.addEventListener("click", () => {
                        loadLifeEvents(celebrity.id, celebrity.fullName);
                    });

                    gallery.appendChild(img);
                }
            });
        })
        .catch(error => console.error(error));

    function loadLifeEvents(celebrityId, fullName) {
        fetch(`/api/Lifeevents/Celebrities/${celebrityId}`)
            .then(response => response.json())
            .then(events => {
                eventsContainer.innerHTML = "";
                eventsContainer.style.display = "block";

                if (events.length === 0) {
                    eventsContainer.innerHTML = "<p>Нет данных о событиях.</p>";
                    return;
                }

                events.forEach(ev => {
                    const row = document.createElement("div");
                    row.style.display = "flex";
                    row.style.gap = "15px";
                    row.style.marginBottom = "5px";

                    const nameCol = document.createElement("div");
                    nameCol.style.width = "200px";
                    nameCol.textContent = fullName;

                    const dateCol = document.createElement("div");
                    dateCol.style.width = "200px";
                    dateCol.textContent = ev.date ? ev.date : "Дата неизвестна";

                    const descCol = document.createElement("div");
                    descCol.textContent = ev.description;

                    row.appendChild(nameCol);
                    row.appendChild(dateCol);
                    row.appendChild(descCol);

                    eventsContainer.appendChild(row);
                });
            })
            .catch(error => {
                console.error("Ошибка загрузки событий:", error);
                eventsContainer.innerHTML = "<p>Ошибка загрузки событий.</p>";
                eventsContainer.style.display = "block";
            });
    }
});