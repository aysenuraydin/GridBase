import { Link } from 'react-router-dom';
import { Card, CardBody, Col, Container, Row } from 'reactstrap';
import { useAuth } from 'context/AuthContext';
import { useGetBrand } from 'hooks/useBrand';
import { useUserProfile } from 'hooks/useUser'; 
import { getUserInitials } from 'common/utils/getUserInitials';
import config from 'config';

const getGreeting = () => {
    const h = new Date().getHours();
    if (h < 6) return "İyi geceler";
    if (h < 12) return "Günaydın";
    if (h < 18) return "İyi günler";
    return "İyi akşamlar";
};


export const Dashboard = () => { 
    const { data: brand } = useGetBrand();
    const { user: usr } = useAuth();
    const { data: user, isLoading: isUserLoading } = useUserProfile(usr?.id ?? "");  

    document.title = "Panel | " + (brand?.companyName || "Gridbase");

    const fullName = [user?.firstName, user?.lastName].filter(Boolean).join(" ") || "Kullanıcı";
    const roles: string[] = user?.roles ?? [];  

    return (
        <div className="page-content">
            <Container fluid>
                <Card className="overflow-hidden">
                    <div className="bg-primary-subtle">
                        <CardBody className="p-4 bg-soft-primary">
                            <Row className="align-items-center">
                                <Col>
                                    <div className="d-flex align-items-center gap-3">
                                        <div className="avatar-md flex-shrink-0">
                                            {!user?.profilePictureUrl ? (
                                                <div className={`avatar-title border border-2 bg-light text-primary rounded-circle text-uppercase`} 
                                                style={{ width: '70px', height: '70px', display: 'flex', alignItems: 'center', justifyContent: 'center' }}>
                                                {getUserInitials(user?.firstName??"", user?.lastName??"")}
                                                </div>
                                            ) : (
                                                <div> 
                                                    <img 
                                                    style={{ width: '70px', height: '70px'}}
                                                    className="rounded-circle header-profile-user border" 
                                                        src={`${config.api.FILE_API_URL}/File/${user.profilePictureUrl}`}
                                                        alt={user?.firstName +" "+ user?.lastName}
                                                    /> 
                                                </div>
                                            )} 
                                        </div>
                                        <div>
                                            <p className="text-muted mb-1">{getGreeting()},</p>
                                            <h4 className="fw-semibold mb-1">
                                                {isUserLoading ? "Yükleniyor..." : fullName} 👋
                                            </h4>
                                            <div className="d-flex flex-wrap justify-content-center justify-content-md-start gap-1">
                                                {roles.map((r) => (
                                                    <span key={r} className="badge bg-primary-subtle text-primary border border-primary-subtle">
                                                        {r}
                                                    </span>
                                                ))}
                                            </div>
                                        </div>
                                    </div>
                                </Col>
                                <Col xs="auto" className="d-none d-md-block">
                                    <div className="text-end">
                                        <h5 className="mb-0 fw-semibold">{brand?.companyName || "Gridbase"}</h5>
                                        <p className="text-muted mb-0 fs-13">
                                            {new Date().toLocaleDateString("tr-TR", {
                                                weekday: "long", day: "numeric", month: "long", year: "numeric",
                                            })}
                                        </p>
                                    </div>
                                </Col>
                            </Row>
                        </CardBody>
                    </div>
                </Card>

                {/* ── Hızlı erişim ── */}
                <h5 className="mb-3 fw-semibold">Hızlı Erişim</h5>
                <div>
                    <Row className="g-3"> 
                        <Col xs={6} sm={4} md={3} lg={2} >
                            <Link to={"/menuitems"} className="text-decoration-none">
                                <Card className="mb-0 h-100 dash-quick">
                                    <CardBody className="text-center p-4">
                                        <div className="avatar-sm mx-auto mb-3">
                                            <div className={`avatar-title bg-secondary-subtle text-secondary rounded fs-22`}>
                                                <i className="ri-list-check-2" />
                                            </div>
                                        </div>
                                        <h6 className="mb-0 text-body">Menü</h6>
                                    </CardBody>
                                </Card>
                            </Link>
                        </Col> 
                        <Col xs={6} sm={4} md={3} lg={2} >
                            <Link to={"/datatables"} className="text-decoration-none">
                                <Card className="mb-0 h-100 dash-quick">
                                    <CardBody className="text-center p-4">
                                        <div className="avatar-sm mx-auto mb-3">
                                            <div className={`avatar-title bg-primary-subtle text-primary rounded fs-22`}>
                                                <i className="ri-table-line" />
                                            </div>
                                        </div>
                                        <h6 className="mb-0 text-body">Tables</h6>
                                    </CardBody>
                                </Card>
                            </Link>
                        </Col> 
                    </Row>
                </div>
            </Container>

            <style>{`
                .dash-quick { transition: transform .2s ease, box-shadow .2s ease; cursor: pointer; }
                .dash-quick:hover {
                    transform: translateY(-4px);
                    box-shadow: 0 8px 24px rgba(0,0,0,.10);
                }
            `}</style>
        </div>
    );
};