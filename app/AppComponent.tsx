import * as React from 'react';

namespace app {

    class Book {
        constructor(id: string, name: string, chapters: number) {
            this.id = id;
            this.name = name;
            this.chapters = chapters;
        }

        id: string; // osisId
        name: string;
        chapters: number;
    }

    function getBooks(): Array<Book> {
        return [
            new Book('Gen', 'Genesis', 50),
            new Book('Exod', 'Exodus', 40),
            new Book('Lev', 'Leviticus', 27),
            new Book('Num', 'Numbers', 36),
            new Book('Deut', 'Deuteronomy', 34),
            new Book('Josh', 'Joshua', 24),
            new Book('Judg', 'Judges', 21),
            new Book('Ruth', 'Ruth', 4),
            new Book('1Sam', '1 Samuel', 31),
            new Book('2Sam', '2 Samuel', 24),
            new Book('1Kgs', '1 Kings', 22),
            new Book('2Kgs', '2 Kings', 25),
            new Book('1Chr', '1 Chronicles', 29),
            new Book('2Chr', '2 Chronicles', 36),
            new Book('Ezra', 'Ezra', 10),
            new Book('Neh', 'Nehemiah', 13),
            new Book('Esth', 'Esther', 10),
            new Book('Job', 'Job', 42),
            new Book('Ps', 'Psalms', 150),
            new Book('Prov', 'Proverbs', 31),
            new Book('Eccl', 'Ecclesiastes', 12),
            new Book('Song', 'Song of Solomon', 8),
            new Book('Isa', 'Isaiah', 66),
            new Book('Jer', 'Jeremiah', 52),
            new Book('Lam', 'Lamentations', 5),
            new Book('Ezek', 'Ezekiel', 48),
            new Book('Dan', 'Daniel', 12),
            new Book('Hos', 'Hosea', 14),
            new Book('Joel', 'Joel', 3),
            new Book('Amos', 'Amos', 9),
            new Book('Obad', 'Obadiah', 1),
            new Book('Jonah', 'Jonah', 4),
            new Book('Mic', 'Micah', 7),
            new Book('Nah', 'Nahum', 3),
            new Book('Hab', 'Habakkuk', 3),
            new Book('Zeph', 'Zephaniah', 3),
            new Book('Hag', 'Haggai', 2),
            new Book('Zech', 'Zechariah', 14),
            new Book('Mal', 'Malachi', 4),
            new Book('Matt', 'Matthew', 28),
            new Book('Mark', 'Mark', 16),
            new Book('Luke', 'Luke', 24),
            new Book('John', 'John', 21),
            new Book('Acts', 'Acts', 28),
            new Book('Rom', 'Romans', 16),
            new Book('1Cor', '1 Corinthians', 16),
            new Book('2Cor', '2 Corinthians', 13),
            new Book('Gal', 'Galatians', 6),
            new Book('Eph', 'Ephesians', 6),
            new Book('Phil', 'Philippians', 4),
            new Book('Col', 'Colossians', 4),
            new Book('1Thess', '1 Thessalonians', 5),
            new Book('2Thess', '2 Thessalonians', 3),
            new Book('1Tim', '1 Timothy', 6),
            new Book('2Tim', '2 Timothy', 4),
            new Book('Titus', 'Titus', 3),
            new Book('Phim', 'Philemon', 1),
            new Book('Heb', 'Hebrews', 13),
            new Book('Jas', 'James', 5),
            new Book('1Pet', '1 Peter', 5),
            new Book('2Pet', '2 Peter', 3),
            new Book('1John', '1 John', 5),
            new Book('2John', '2 John', 1),
            new Book('3John', '3 John', 1),
            new Book('Jude', 'Jude', 1),
            new Book('Rev', 'Revelation', 22)
        ];
    }

    function getBookById(id: string) {
        var b = new Book();
        b.chapters = 1;
        return b;
    }

    class NextChapter {
        constructor(bookId: string, chapter: number) {
            this.bookId = bookId;
            this.chapter = chapter;
        }

        bookId: string;
        chapter: number;
    }

    export class List {
        constructor(name: string, ...bookIds: Array<string>) {
            this.name = name;
            this.bookIds = bookIds;
            this.totalChapters = 0;
            bookIds.map(id => {
               this.totalChapters += getBookById(id).chapters;
            });
            this.next = new NextChapter(bookIds[0], 1);
        }

        name: string;
        bookIds: Array<string>;
        totalChapters: number;
        next: NextChapter;
    }

    function getGrantHornerLists(): Array<List> {
        return [
            new List('List 1', 'Matt', 'Mark', 'Luke', 'John'),
            new List('List 2', 'Gen', 'Exod', 'Lev', 'Num', 'Deut'),
            new List('List 3', 'Rom', '1Cor', '2Cor', 'Gal', 'Eph', 'Phil', 'Col', 'Heb'),
            new List('List 4', '1Thess', '2Thess', '1Tim', '2Tim', 'Titus', 'Phim', 'Jas', '1Pet', '2Pet', '1John', '2John', '3John', 'Jude', 'Rev'),
            new List('List 5', 'Job', 'Eccl', 'Song'),
            new List('List 6', 'Ps'),
            new List('List 7', 'Prov'),
            new List('List 8', 'Josh', 'Judg', 'Ruth', '1Sam', '2Sam', '1Kgs', '2Kgs', '1Chr', '2Chr', 'Ezra', 'Neh', 'Esth'),
            new List('List 9', 'Isa', 'Jer', 'Lam', 'Ezek', 'Dan', 'Hos', 'Joel', 'Amos', 'Obad', 'Jonah', 'Mic', 'Nah', 'Hab', 'Zeph', 'Hag', 'Zech', 'Mal'),
            new List('List 10', 'Acts')
        ];
    }

    export class AppComponent extends React.Component<{}, {}> {
        render() {

            var lists = getGrantHornerLists();

            return (
                <div>
                    <h1>My Reading Lists</h1>
                    <div className="list-container">
                    { lists.map(list => {
                        return <ListComponent list={list} key={list.name} />;
                    }) }
                    </div>
                </div>
            );
        }
    }

    class ListComponent extends React.Component<{ list: List }, {}> {
        render() {

            
            
            var preBooks = [];
            var postBooks = [];
            var nextBook = getBookById(this.props.list.next.bookId).name;
            
            return (
                <div className="list">
                    <h1>{this.props.list.name}</h1>
                    <span className="days">({this.props.list.totalChapters} days)</span>
                    <p className="books">{this.props.list.bookIds}</p>
                </div>
            );
        }
    }

}