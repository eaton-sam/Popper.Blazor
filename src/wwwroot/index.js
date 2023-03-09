import './popper.min.js'

export function createPopper(element, content, settings, autoClose, dotnetRef) {
    Popper.createPopper(element, content, settings);

    let isChildOfContent = (element) => {
        if (element === undefined || element === null) {
            return false;
        }

        if (element === content) {
            return true;
        }

        return isChildOfContent(element.parentElement);
    }

    let offClick = (e) => {
        if (isChildOfContent(e.target)) {
            return;
        }

        dotnetRef.invokeMethodAsync('HidePopper').then(() => {
            document.removeEventListener('click', offClick);
        });
    };

    if (autoClose) {
        document.addEventListener('click', offClick);
    }
}