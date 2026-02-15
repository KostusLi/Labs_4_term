interface User {
    name: string;
    age: number;
}

let user: User = {
    name: 'Masha',
    age: 21
};

let numbers: number[] = [1, 2, 3];


interface Location1 {
    city: string;
    country: string;
}

type Location2 = {
    city: string;
    country: string;
}

interface UserWithLocation extends User {
    location: Location1;
}

let user1: UserWithLocation = {
    name: 'Masha',
    age: 23,
    location: {
        city: 'Minsk',
        country: 'Belarus'
    }
};



interface UserWithSkills extends User {
    skills: string[];
}

let user2: UserWithSkills = {
    name: 'Masha',
    age: 28,
    skills: ["HTML", "CSS", "JavaScript", "React"]
};



interface Student {
    id: number;
    name: string;
    group: number;
}

const array: Student[] = [
    { id: 1, name: 'Vasya', group: 10 },
    { id: 2, name: 'Ivan', group: 11 },
    { id: 3, name: 'Masha', group: 12 },
    { id: 4, name: 'Petya', group: 10 },
    { id: 5, name: 'Kira', group: 11 },
];



interface ExamsSimple {
    maths: boolean;
    programming: boolean;
}

interface StudiesSimple {
    university: string;
    speciality: string;
    year: number;
    exams: ExamsSimple;
}

interface UserWithStudiesSimple extends User {
    studies: StudiesSimple;
}

let user4: UserWithStudiesSimple = {
    name: 'Masha',
    age: 19,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        exams: {
            maths: true,
            programming: false
        }
    }
};


interface Exam {
    maths?: boolean;
    programming?: boolean;
    mark: number;
}

interface Department {
    faculty: string;
    group: number;
}

interface StudiesWithExams {
    university: string;
    speciality: string;
    year: number;
    department: Department;
    exams: Exam[];
}

interface UserWithStudies extends User {
    studies: StudiesWithExams;
}

let user5: UserWithStudies = {
    name: 'Masha',
    age: 22,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            { maths: true, mark: 8 },
            { programming: true, mark: 4 },
        ]
    }
};


interface Professor {
    name: string;
    degree: string;
}

interface ExamWithProfessor extends Exam {
    professor: Professor;
}

interface StudiesWithProfessors extends Omit<StudiesWithExams, "exams"> {
    exams: ExamWithProfessor[];
}

interface UserWithProfessors extends User {
    studies: StudiesWithProfessors;
}

let user6: UserWithProfessors = {
    name: 'Masha',
    age: 21,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {
                maths: true,
                mark: 8,
                professor: {
                    name: 'Ivan Ivanov',
                    degree: 'PhD'
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Petrov',
                    degree: 'PhD'
                }
            },
        ]
    }
};


interface Article {
    title: string;
    pagesNumber: number;
}

interface ProfessorWithArticles extends Professor {
    articles: Article[];
}

interface ExamWithArticles extends Exam {
    professor: ProfessorWithArticles;
}

interface StudiesFull extends Omit<StudiesWithExams, "exams"> {
    exams: ExamWithArticles[];
}

interface UserFull extends User {
    studies: StudiesFull;
}

let user7: UserFull = {
    name: 'Masha',
    age: 20,
    studies: {
        university: 'BSTU',
        speciality: 'designer',
        year: 2020,
        department: {
            faculty: 'FIT',
            group: 10,
        },
        exams: [
            {
                maths: true,
                mark: 8,
                professor: {
                    name: 'Ivan Petrov',
                    degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 },
                    ]
                }
            },
            {
                programming: true,
                mark: 10,
                professor: {
                    name: 'Petr Ivanov',
                    degree: 'PhD',
                    articles: [
                        { title: "About HTML", pagesNumber: 3 },
                        { title: "About CSS", pagesNumber: 5 },
                        { title: "About JavaScript", pagesNumber: 1 },
                    ]
                }
            },
        ]
    }
};



interface Post {
    id: number;
    message: string;
    likesCount: number;
}

interface Dialog {
    id: number;
    name: string;
}

interface Message {
    id: number;
    name: string;
}

interface Store {
    state: {
        profilePage: {
            posts: Post[];
            newPostText: string;
        };
        dialogsPage: {
            dialogs: Dialog[];
            messages: Message[];
        };
        sidebar: unknown[];
    };
}

let store: Store = {
    state: {
        profilePage: {
            posts: [
                { id: 1, message: 'Hi', likesCount: 12 },
                { id: 2, message: 'By', likesCount: 1 },
            ],
            newPostText: 'About me',
        },
        dialogsPage: {
            dialogs: [
                { id: 1, name: 'Valera' },
                { id: 2, name: 'Andrey' },
                { id: 3, name: 'Sasha' },
                { id: 4, name: 'Viktor' },
            ],
            messages: [
                { id: 1, name: 'hi' },
                { id: 2, name: 'hi hi' },
                { id: 3, name: 'hi hi hi' },
            ],
        },
        sidebar: [],
    }
};



function deepClone<T>(obj: T): T {
    if (Array.isArray(obj)) {
        return obj.map(item => deepClone(item)) as T;
    }

    if (obj !== null && typeof obj === "object") {
        const result: any = {};
        for (const key in obj) {
            result[key] = deepClone((obj as any)[key]);
        }
        return result;
    }

    return obj;
}

let temp:Store = deepClone(store);
console.log(temp);