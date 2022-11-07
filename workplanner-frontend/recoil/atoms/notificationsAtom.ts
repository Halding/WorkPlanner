import { atom } from 'recoil'
import { Notification } from "../../hooks/useNotifications"

export const notificationsState = atom<Notification[]>({
    key: 'notificationsState', // unique ID (with respect to other atoms/selectors)
    default: [], // default value (aka initial value)
})
