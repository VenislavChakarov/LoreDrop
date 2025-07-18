document.addEventListener("DOMContentLoaded", function () {
    const starContainers = document.querySelectorAll('.star-container');
    const starRatingEl = document.querySelector('.star-rating');
    const seriesIdEl = document.getElementById('watchlist-btn');
    const seriesId = seriesIdEl
        ? seriesIdEl.dataset.seriesId
        : starRatingEl.getAttribute('data-series-id');
    // Read and store initial rating for persistence
    let currentRating = parseFloat(starRatingEl.getAttribute('data-current-rating')) || 0;
    updateStars(currentRating);

    // Hover preview: half/full star on left/right
    starRatingEl.addEventListener('mousemove', function (e) {
        const rect = starRatingEl.getBoundingClientRect();
        const x = e.clientX - rect.left;
        let hoverRating = Math.ceil((x / rect.width) * 5 * 2) / 2;
        updateStars(hoverRating);
    });
    starRatingEl.addEventListener('mouseleave', function () {
        updateStars(currentRating);
    });

    // Click handlers
    starContainers.forEach(container => {
        const fullStar = container.querySelector('i:first-child');
        const halfStar = container.querySelector('.half-star');

        fullStar.addEventListener('click', function () {
            const rating = parseInt(container.dataset.rating);
            submitRating(rating);
        });
        halfStar.addEventListener('click', function (e) {
            e.stopPropagation();
            const rating = parseInt(container.dataset.rating) - 0.5;
            submitRating(rating);
        });
    });

    function submitRating(rating) {
        fetch('/Details/Rate', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
                'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
            },
            body: JSON.stringify({ seriesId: seriesId, rating: rating })
        })
            .then(response => {
                if (!response.ok) throw new Error("Rating failed");
                currentRating = rating;
                updateStars(rating);
                animateStars();
            })
            .catch(error => console.error("Rating error:", error));
    }

    function updateStars(rating) {
        starContainers.forEach((container, i) => {
            const fullStar = container.querySelector('i:first-child');
            const halfStar = container.querySelector('.half-star');
            const starIndex = i + 1;

            if (rating >= starIndex) {
                fullStar.className = "fas fa-star";
                halfStar.style.opacity = 0;
            } else if (rating >= starIndex - 0.5) {
                fullStar.className = "far fa-star";
                halfStar.style.opacity = 1;
            } else {
                fullStar.className = "far fa-star";
                halfStar.style.opacity = 0;
            }
        });
    }

    function animateStars() {
        starContainers.forEach(container => {
            container.classList.add('star-animate');
            setTimeout(() => container.classList.remove('star-animate'), 500);
        });
    }
    // Add comment
    const commentInput = document.getElementById("new-comment-input");
    const postCommentBtn = document.getElementById("post-comment-btn");
    const commentsList = document.getElementById("comments-list");
    const commentCount = document.getElementById("comment-count");

    if (postCommentBtn) {
        postCommentBtn.addEventListener("click", function () {
            const text = commentInput.value.trim();
            if (text === "") return;

            fetch('/Details/AddCommentAjax', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                    'RequestVerificationToken': document.querySelector('input[name="__RequestVerificationToken"]').value
                },
                body: JSON.stringify({
                    seriesId: seriesId,
                    text: text
                })
            })
                .then(res => res.json())
                .then(data => {
                    const newComment = document.createElement("div");
                    newComment.classList.add("comment-item");
                    newComment.innerHTML = `
          <div class="comment-header">
            <span class="comment-author">${data.authorName}</span>
            <time class="comment-date">${data.createdOn}</time>
          </div>
          <div class="comment-body">${data.text}</div>
        `;
                    commentsList.prepend(newComment);
                    commentInput.value = "";
                    commentCount.textContent = parseInt(commentCount.textContent) + 1;
                })
                .catch(error => console.error("Error adding comment:", error));
        });
    }
    
});