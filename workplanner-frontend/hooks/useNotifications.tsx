import {
    CheckCircleIcon,
    ExclamationCircleIcon,
    InformationCircleIcon,
    XMarkIcon,
} from '@heroicons/react/24/solid';
import { motion } from 'framer-motion';
import React from 'react';
import { useRef } from 'react';
import { useRecoilState } from 'recoil';
import { notificationsState } from '../recoil/atoms/notificationsAtom';

export type Notification = {
    id: number;
    title: string;
    description: string;
    type?: NotificationType;
    timeout?: number;
    remove(id: number): void;
};

export enum NotificationType {
    'normal',
    'success',
    'error',
}

type PushNotificationProps = {
    title: string;
    description: string;
    type?: NotificationType;
    timeout?: number;
};

export default function useNotifications() {
    const [notifications, setNotifications] =
        useRecoilState<Notification[]>(notificationsState);

    const count = useRef(0);
    const notificationsRef = useRef<Notification[]>();
    notificationsRef.current = notifications;

    const pushNotification = ({
                                  title,
                                  description,
                                  type = NotificationType.normal,
                                  timeout = 3000,
                              }: PushNotificationProps) => {
        const notification: Notification = {
            id: ++count.current,
            title,
            description,
            type,
            timeout,
            remove: popNotification,
        };

        setNotifications([...notifications, notification]);
        if (timeout > 0)
            setTimeout(() => {
                popNotification(notification.id);
            }, timeout);
    };

    const popNotification = (id: number) => {
        const list = [...notificationsRef.current!];
        const index = list.findIndex((x) => x.id === id);
        if (index > -1) {
            list.splice(index, 1);
            setNotifications(list);
        }
    };

    return { pushNotification };
}

export const RenderNotification = React.forwardRef<HTMLLIElement, Notification>(
    (props, ref) => {
        const typeIcon = () => {
            switch (props.type) {
                case NotificationType.normal:
                    return (
                        <InformationCircleIcon
                            className="w-6"
                            stroke="#0ea5e9"
                            fill="white"
                        />
                    );
                case NotificationType.success:
                    return (
                        <CheckCircleIcon className="w-6" stroke="#22c55e" fill="white" />
                    );
                case NotificationType.error:
                    return (
                        <ExclamationCircleIcon
                            className="w-6"
                            stroke="#f43f5e"
                            fill="white"
                        />
                    );
                default:
                    return (
                        <InformationCircleIcon
                            className="w-6"
                            stroke="#0ea5e9"
                            fill="white"
                        />
                    );
            }
        };

        return (
            <motion.li
                ref={ref}
                layout
                // initial={{ scale: 0.8, opacity: 0 }}
                animate={{ scale: 1, opacity: 1 }}
                exit={{ scale: 0.8, opacity: 0 }}
                transition={{ type: 'spring' }}
                key={props.id}
                className="pointer-events-auto z-50 w-full max-w-sm overflow-hidden rounded-lg bg-white shadow-lg ring-1 ring-black ring-opacity-5"
            >
                <div className="p-4">
                    <div className="flex items-start">
                        <div className="flex-shrink-0">{typeIcon()}</div>
                        <div className="ml-3 w-0 flex-1 pt-0.5">
                            <p className="text-sm font-medium text-slate-900">
                                {props.title}
                            </p>
                            <p className="mt-1 text-sm text-slate-500">{props.description}</p>
                        </div>
                        <div className="ml-4 flex flex-shrink-0">
                            <button
                                type="button"
                                className="inline-flex rounded-md bg-white text-slate-400 hover:text-slate-500 focus:outline-none focus:ring-0"
                                onClick={() => props.remove(props.id)}
                            >
                                <span className="sr-only">Close</span>
                                <XMarkIcon className="h-5 w-5" aria-hidden="true" />
                            </button>
                        </div>
                    </div>
                </div>
            </motion.li>
        );
    }
);

RenderNotification.displayName = 'RenderNotificationDisplayName';
