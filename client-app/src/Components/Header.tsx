import Authorization from "../Authorization.tsx";
import {useState} from "react";

export function Header() {
    const [activeButton, setActiveButton] = useState('')

    function MakeActive(value: string) {
        setActiveButton(value)
    }

    return (
        <div className="row">
            <nav className="navbar navbar-expand-md navbar-light bg-light">
                <a className="navbar-brand" href="#">StudyLangWithGames</a>
                <button className="navbar-toggler" type="button" data-toggle="collapse"
                        data-target="#navbarSupportedContent" aria-controls="navbarSupportedContent"
                        aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>

                <div className="collapse navbar-collapse" id="navbarSupportedContent">
                    <ul className="navbar-nav mr-auto">
                        <li className="nav-item active">
                            <a className="nav-link" href="/study">My study </a>
                        </li>
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle" href="study/games" id="navbarDropdown" role="button"
                               data-bs-toggle="dropdown" aria-expanded="false">
                                Games
                            </a>
                            <div className="dropdown-menu" aria-labelledby="navbarDropdown">
                                <a className="dropdown-item" href="./study/games/words">Words</a>
                                <a className="dropdown-item" href="./study/games/crosswords">Crosswords</a>
                                <div className="dropdown-divider"></div>
                                <a className="dropdown-item" href="#">Something else here</a>
                            </div>
                        </li>
                        <li className="nav-item">
                            <a className={`nav-link ${activeButton === 'faq' ? "active" : ''}`} href="/faq"
                               onClick={() => MakeActive('faq')}>FAQ</a>
                        </li>
                        <li className="nav-item">
                            <a className={`nav-link ${activeButton === 'contacts' ? 'active' : ''}`}
                               href="/contacts"
                               onClick={() => MakeActive('contacts')}>Contacts</a>
                        </li>
                    </ul>
                    <div className="col text-end">
                        <Authorization/>
                    </div>
                </div>
            </nav>
        </div>
    );
}