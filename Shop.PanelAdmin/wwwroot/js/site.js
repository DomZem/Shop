// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// highlighting links in the header
document.addEventListener('DOMContentLoaded', () => {
    const currentPath = location.pathname.toLowerCase();
    document.querySelectorAll('.nav-link').forEach(link => {
        const linkPath = link.getAttribute('href').toLowerCase();

        // Ensure linkPath ends with a trailing slash or is exactly equal to the currentPath
        const isExactMatch = currentPath === linkPath;
        const isSubPathMatch = currentPath.startsWith(linkPath) && (currentPath[linkPath.length] === '/' || !currentPath[linkPath.length]);

        if (isExactMatch || isSubPathMatch) {
            link.classList.add('active');
        } else {
            link.classList.remove('active');
        }
    });
});