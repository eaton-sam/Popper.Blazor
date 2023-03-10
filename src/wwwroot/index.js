import './popper.min.js'

let popperMap = new Map();

export function createPopper(anchor, content, settings, autoClose, dotnetRef) {
    popperMap.set(content, Popper.createPopper(anchor, content, settings));

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