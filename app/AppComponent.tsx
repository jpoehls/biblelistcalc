import * as React from 'react';

namespace app {
    
    class Book {
        id: string; // osisId
        name: string;
        chapters: number;
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