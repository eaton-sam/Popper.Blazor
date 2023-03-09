
module.exports = {
    content: [
        "./**/*.razor",
        "./wwwroot/index.html"
    ],
    plugins: [
        require('@tailwindcss/line-clamp'),
        require('@tailwindcss/typography')
    ],
}
